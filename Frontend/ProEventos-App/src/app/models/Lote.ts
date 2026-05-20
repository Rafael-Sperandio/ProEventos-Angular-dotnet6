import { Evento } from "./Evento";

export interface Lote {
  id: Number ;
  nome: string ;
  preco: Number ;
  dataInicio?: Date ;
  dataFim?: Date ;
  quantidade: Number ;
  eventoId: Number ;
  evento?: Evento ;

}
