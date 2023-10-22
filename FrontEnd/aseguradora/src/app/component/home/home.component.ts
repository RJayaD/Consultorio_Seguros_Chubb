import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SeguroClientesService } from 'src/app/services/seguroclientes.services';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  //Datos de la tabla
  seguroclientes:any[];
  segurocliente:any;
  //
  filtro: string = '';
  //ComboBox
  //Declarar servicio
  private servSeguroClientes: SeguroClientesService;
  constructor(private router:Router, private serSeguroClientes:SeguroClientesService) {
    this.seguroclientes=[];
    this.servSeguroClientes=serSeguroClientes;

  }

  ngOnInit(): void {
    
  }
  
  Buscar(even: any) {
    const dato = this.filtro;
    if(dato.trim() !== '')
    {
    this.seguroclientes = [];
    this.servSeguroClientes.getSeguroClientesxCondicional(dato).subscribe({
      next: (response) => {
        this.seguroclientes = response;
      }
    }
    );
    }
    else{
      this.seguroclientes = [];
    }
  }
  

}
