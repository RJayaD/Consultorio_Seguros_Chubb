import { Binary } from "@angular/compiler";

export class Seguro {
  SeguroId:number;
  NombreSeguro: string;
  Codigo: string;
  FechaCreacion: string;
  Suma: number;
  Prima: number;
  Estado: string;


  constructor() {
    this.SeguroId=0;
    this.NombreSeguro = '';
    this.Codigo = '';
    this.FechaCreacion= '';
    this.Suma = 0;
    this.Prima = 0;
    this.Estado='';
  }
}




