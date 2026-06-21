import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Evento } from '@app/models/Evento';

import { EventoService } from '../../../services/evento.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';
import { environment } from '../../../../environments/environment';
import { PaginatedResult, Pagination } from '../../../models/Pagination';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Subject } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
  //providers: [EventoService]
})
export class EventoListaComponent implements OnInit {
  public eventoId = 0;
  public pagination = {} as Pagination;
  modalRef?: BsModalRef;
  public eventos: Evento[] = [];

  public larguraImagem = 150;
  public margemImagem = 2;
  public exibirImagem = true;

  termoBuscaChanged: Subject<string> = new Subject<string>();
  public filtrarEventos(evt: any): void {
    if (this.termoBuscaChanged.observers.length === 0) {
      this.termoBuscaChanged
        .pipe(debounceTime(1500))
        .subscribe((filtrarPor) => {
          this.spinner.show();
          this.eventoService
            .getEventos(
              this.pagination.currentPage,
              this.pagination.itemsPerPage,
              filtrarPor
            )
            .subscribe(
              (paginatedResult: PaginatedResult<Evento[]>) => {
                this.eventos = paginatedResult.result;
                this.pagination = paginatedResult.pagination;
              },
              (error: any) => {
                this.spinner.hide();
                this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
              }
            )
            .add(() => this.spinner.hide());
        });
    }
    this.termoBuscaChanged.next(evt.value);
  }

  constructor(private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    private router: Router
  ) { }

  public ngOnInit(): void {
    this.spinner.show();
    this.pagination = {
      currentPage: 1,
      itemsPerPage: 3,
      totalItems: 1,
    } as Pagination;
    // setTimeout(() => {
    //   /* spinner ends after 5 seconds */
    //   this.spinner.hide();
    // }, 5000);
    this.carregarEventos();


  }
  public carregarEventos(): void{
    this.spinner.show();
    this.eventoService
      .getEventos(this.pagination.currentPage, this.pagination.itemsPerPage)
      .subscribe({
      next: (paginatedResult: PaginatedResult<Evento[]>) => {
          this.eventos = paginatedResult.result;
          this.pagination = paginatedResult.pagination;
      },
      error: (error: any) => {
        this.spinner.hide();
        this.toastr.error('Erro ao Carregar os Eventos', 'Erro!');
      },
  }).add(()=> {  this.spinner.hide();});

  }
  public alterarImagem(): void{
    this.exibirImagem = !this.exibirImagem;
  }

  public mostraImagem(imagemURL: string): string {
    console.log(`${environment.apiURL}resources/images/${imagemURL}`)
    return (imagemURL !== '')
      ? `${environment.apiURL}resources/images/${imagemURL}`
      : 'assets/img/semImagem.jpeg';
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number): void {
    event.stopPropagation();
    this.eventoId = eventoId;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  public pageChanged(event: any): void {
    const pageEvent = event as PageChangedEvent;//TODO casting não é o ideal
    if(pageEvent && pageEvent.page){
      this.pagination.currentPage = pageEvent.page;
      this.carregarEventos();
    }
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();
    this.eventoService.deleteEvento(this.eventoId).subscribe({
    next: (result: any) => {
      if (result.message === 'Deletado') {
          this.toastr.success('O Evento foi deletado com Sucesso.', 'Deletado!');
          this.carregarEventos();
        }
    },
    error:  (error: any) => {
      console.error(error);
      this.toastr.error(`Erro ao tentar deletar o evento ${this.eventoId}`, 'Erro');
    },
    //complete: () => this.spinner.hide(),
    }).add(() => this.spinner.hide());


  }

  decline(): void {
    this.modalRef?.hide();
  }
  detalheEvento(id: number): void{
    this.router.navigate([`eventos/detalhe/${id}`]);
  }

}
