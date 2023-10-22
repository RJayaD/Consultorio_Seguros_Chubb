
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Asegurado } from 'src/app/Models/Asegurado';
import { AseguradosService } from 'src/app/services/asegurados.service';

@Component({
  selector: 'app-asegurados',
  templateUrl: './asegurados.component.html',
  styleUrls: ['./asegurados.component.css']
})
export class AseguradosComponent implements OnInit{

  banNumerosCedula = false;
  banNumerosTelefono = false;
  asegurado: Asegurado;
  aseguradoId: any;
  txtbt: string = "Registrar Asegurado";
  mostrarCampo: Boolean = true;
  AseguradoForm!: FormGroup;

  constructor(private servAsegurado: AseguradosService, private formBuilder: FormBuilder, private router: Router, private routeData: ActivatedRoute) {
    this.asegurado = new Asegurado();
    this.routeData.params
      .subscribe(params => {
        this.aseguradoId = params['id']
      }
      );
    if (typeof this.aseguradoId !== 'undefined') {
      this.buscaId();
      this.mostrarCampo = false;
      this.txtbt = "Actualizar Asegurado";
    }
  }

  ngOnInit(): void {
    if(typeof this.aseguradoId !== 'undefined'){
    this.AseguradoForm = this.formBuilder.group({
      cedula: [this.asegurado.cedula, [Validators.required,Validators.pattern('^[0-9]*$')]],
      nombre_cliente: [this.asegurado.nombre_cliente, [Validators.required,Validators.pattern('^[a-zA-Z ]*$')]],
      telefono: ['', [Validators.required, ]],
      edad: [0, [Validators.required]]
    });
  }
  else{
    this.AseguradoForm = this.formBuilder.group({
      cedula: [this.asegurado.cedula, [Validators.required, Validators.pattern('^[0-9]*$')]],
      nombre_cliente: [this.asegurado.nombre_cliente, [Validators.required, Validators.pattern('^[a-zA-Z ]*$')] ],
      fecha_creacion: [new Date().toISOString().split('T')[0], [Validators.required, ]],
      telefono: [this.asegurado.edad, [Validators.required, Validators.pattern('^[0-9]*$')]],
      edad: [0, [Validators.required]]
    });
  }
  }
  



  submitForm() {

    
    if (this.AseguradoForm.valid) {
      if (typeof this.aseguradoId !== 'undefined') {
      this.AseguradoForm.addControl('AseguradoId', this.formBuilder.control(this.aseguradoId));
      console.log(this.AseguradoForm.value);
      this.servAsegurado.editAsegurado(this.AseguradoForm.value)
      .subscribe({
        next: (response) => {
          this.router.navigate(['/listaAsegurado']);
        },
        error: (error) => {
          alert('Cédula ya existe');
        }
      });
      
      }
      else{
        this.servAsegurado.saveAsegurado(this.AseguradoForm.value)
        .subscribe(
          (response) => {
          this.router.navigate(['/listaAsegurado']);
        },
        (error) => {
          if (error.status === 400) {
            const errorMessage = error.error.msg;
           alert(errorMessage);
          }
        }
      );
      }
    }
    else alert('Formulario Vacio/Datos Incompletos o Errones');
  }


  buscaId() {
    this.servAsegurado.buscarAseguradoId(this.aseguradoId).subscribe({
      next: (response) => {
        this.asegurado.cedula = response.cedula;
        this.asegurado.nombre_cliente = response.nombre_cliente;
        this.asegurado.telefono = response.telefono;
        this.asegurado.edad = response.edad;
        this.AseguradoForm.touched;
      }
    }
    );
  }
  validaNumeroT(even: any) {
    const dato = this.AseguradoForm.value;
    this.banNumerosTelefono = !dato.telefono.split('').every((caracter: string) => !isNaN(Number(caracter)));
  }
  validaNumeroC(even: any) {
    const dato = this.AseguradoForm.value;
    this.banNumerosCedula = !dato.cedula.split('').every((caracter: string) => !isNaN(Number(caracter)));
  }
}
