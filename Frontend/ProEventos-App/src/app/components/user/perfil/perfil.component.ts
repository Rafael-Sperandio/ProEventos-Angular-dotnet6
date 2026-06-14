import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '../../../helpers/ValidatorField';
import { AccountService } from '../../../services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserUpdate } from '../../../models/identity/UserUpdate';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  userUpdate = {} as UserUpdate;
  form!: FormGroup;
  get f(): any {
    return this.form.controls;
  }
  constructor(
      private fb: FormBuilder,
    public accountService: AccountService,
    private router: Router,
    private toaster: ToastrService,
    private spinner: NgxSpinnerService
  ) {}

  ngOnInit(): void {
    this.validation();
    this.carregarUsuario();
  }

  private carregarUsuario(): void {
    this.spinner.show();
    this.accountService
      .getUser()
      .subscribe(
        (userRetorno: UserUpdate) => {
          console.log(userRetorno);
          this.userUpdate = userRetorno;
          this.form.patchValue(this.userUpdate);
          this.toaster.success('Usuário Carregado', 'Sucesso');
        },
        (error) => {
          console.error(error);
          this.toaster.error('Usuário não Carregado', 'Erro');
          this.router.navigate(['/dashboard']);
        }
      )
      .add(() => this.spinner.hide());
  }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('password', 'confirmePassword')
    };
    this.form =  this.fb.group({
      userName: [''],
      titulo: ['NaoIformado', [Validators.required]],
      primeiroNome: ['', [Validators.required]],
      ultimoNome: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required]],
      descricao: ['', [Validators.required]],
      funcao: ['NaoIformado', [Validators.required]],
      password: ['', [Validators.minLength(4)]],
      confirmePassword: [''],
    },formOptions);
    //  confirmePassword: ['', Validators.nullValidator],
    //  password: ['', [Validators.minLength(4), Validators.nullValidator]],

  }

  onSubmit(): void {
    console.log('tentado mudar usuario')
    // Vai parar aqui se o form estiver inválido
    if (this.form.invalid) {
      console.log()
      this.toaster.error("formulario invalido")
      return;
    }
    this.atualizarUsuario();
  }

  public atualizarUsuario() {
    this.userUpdate = { ...this.form.value };
    this.spinner.show();
    this.accountService
      .updateUser(this.userUpdate)
      .subscribe(
        () => this.toaster.success('Usuário atualizado!', 'Sucesso'),
        (error) => {
          this.toaster.error(error.error);
          console.error(error);
        }
      )
      .add(() => this.spinner.hide());
  }

  public resetForm(event: any): void {
    event.preventDefault();
    this.form.reset();
  }

}
