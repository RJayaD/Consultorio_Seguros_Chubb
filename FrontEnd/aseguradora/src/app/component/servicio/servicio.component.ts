import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';
import { AseguradosService } from 'src/app/services/asegurados.service';
import { SegurosService } from 'src/app/services/seguros.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SeguroClientesService } from 'src/app/services/seguroclientes.services';

@Component({
  selector: 'app-servicio',
  templateUrl: './servicio.component.html',
  styleUrls: ['./servicio.component.css']
})
export class ServicioComponent implements OnInit {

  seguros: any[]= [];;
  asegurados: any[]= [];;
  SeguroId: any;
  AseguradoId: any;
  filtro: string = '';
  filtroAseg: string = '';
  //Declarar servicio
  private servSeguro: SegurosService;
  private servAsegurado: AseguradosService;
  private servSeguroCliente: SeguroClientesService;
  SeguroClienteForm!: FormGroup;
  constructor(private router: Router,private serSeguroCliente: SeguroClientesService,private serSeguro: SegurosService,private serAsegurado: AseguradosService,private formBuilder: FormBuilder) { 
    this.seguros= [];
    this.asegurados= [];
    this.servSeguro = serSeguro;
    this.servAsegurado=serAsegurado;
    this.servSeguroCliente=serSeguroCliente;
    
    
  }

  ngOnInit(): void {

    this.SeguroClienteForm=this.formBuilder.group({
     SeguroId: ['', [Validators.required]],
      AseguradoId: ['', [Validators.required]],
    });
  }
  SelectSeguro(seguros: any) {
    console.log(seguros.seguroId);
    this.SeguroId=seguros.seguroId;
    this.SeguroClienteForm.get('SeguroId')?.setValue(seguros.seguroId);

  }
  SelectAsegurado(asegurados: any) {
    this.AseguradoId=asegurados.aseguradoId;
    this.SeguroClienteForm.get('AseguradoId')?.setValue(asegurados.aseguradoId);
    
  }
  contratar() {
    if (typeof this.SeguroId !== 'undefined' && typeof this.AseguradoId !== 'undefined') {
      console.log(this.SeguroClienteForm.value);
      this.servSeguroCliente.saveSeguroCliente(this.SeguroClienteForm.value).subscribe({
        next: (response) => {
          this.seguros = response;
          this.router.navigate(['/']);
    },
    error: (error) => {
      alert('Error al guardar cliente-asegurado, ya existe la relacion');
    }
  }
  );
    }
    else {
      alert("Seleccionar Seguro y Cliente");
    }
}
  Buscar(even: any) {
      const dato = this.filtro;
      if(dato.trim() !== '')
      {
      this.seguros = [];
      this.servSeguro.buscarSeguroCondicion(dato).subscribe({
        next: (response) => {
          this.seguros = response;
        }
      }
      );
      }
      else{
        this.seguros = [];
      }
    }
    BuscarAsegurado(even: any) {
      const dato = this.filtroAseg;
      if(dato.trim() !== '')
      {
      this.asegurados = [];
      this.servAsegurado.buscarAseguradoCondicion(dato).subscribe({
        next: (response) => {
          this.asegurados = response;
        }
      }
      );
      }
      else{
        this.asegurados = [];
      }
    }

  }

