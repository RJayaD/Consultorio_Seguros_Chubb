import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { SegurosService } from 'src/app/services/seguros.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Seguro } from 'src/app/Models/Seguro';


@Component({
  selector: 'app-seguro',
  templateUrl: './seguro.component.html',
  styleUrls: ['./seguro.component.css']
})
export class SeguroComponent implements OnInit {
  seguro: Seguro;
  seguroId: any;
  seguroForm!: FormGroup;
  mostrarCampo: Boolean = true;
  banNummeros = false;
  txtbt: string = "Registrar Seguro";
  constructor(private router: Router, private routeData: ActivatedRoute, private servSeguros: SegurosService, private formBuilder: FormBuilder) {
    this.seguro = new Seguro();
    this.routeData.params
      .subscribe(params => {
        this.seguroId = params['id']
      }
      );
    if (typeof this.seguroId !== 'undefined') {
      this.buscaId();
      this.mostrarCampo = false;
      this.txtbt = "Actualizar Seguro";
    }

  }

  ngOnInit(): void {
    if (typeof this.seguroId !== 'undefined') {
      this.seguroForm = this.formBuilder.group({
        nombre_seguro: [this.seguro.NombreSeguro, [Validators.required, Validators.pattern('^[a-zA-Z ]*$')]],
        codigo: [this.seguro.Codigo, [Validators.required, Validators.minLength(10), Validators.pattern('^[0-9]*$')]],
        suma: ['', [Validators.required]],
        prima: ['', [Validators.required]]
      });
    }
    else {
      this.seguroForm = this.formBuilder.group({
        nombre_seguro: [this.seguro.NombreSeguro, [Validators.required,Validators.pattern('^[a-zA-Z ]*$')]],
        codigo: [this.seguro.Codigo, [Validators.required, Validators.pattern(/^[0-9]+$/)]],
        fecha_creacion: [new Date().toISOString().split('T')[0], [Validators.required, Validators.minLength(10)]],
        suma: ['', [Validators.required]],
        prima: ['', [Validators.required]]
      });
    }
  }





  buscaId() {
    this.servSeguros.buscarSeguroId(this.seguroId).subscribe({
      next: (response) => {
        this.seguro.NombreSeguro = response.nombre_seguro;
        this.seguro.Codigo = response.codigo;
        this.seguro.Suma = response.suma;
        this.seguro.Prima = response.prima;
        this.seguroForm.touched;
      }
    }
    );
  }
  submitForm() {
    console.log(this.seguroForm.value);
    if (this.seguroForm.valid) {
      if (typeof this.seguroId !== 'undefined') {
        this.seguroForm.addControl('seguroId', this.formBuilder.control(this.seguroId));
        console.log(this.seguroForm.value);
        this.servSeguros.editSeguro(this.seguroForm.value)
          .subscribe({
            next: (response) => {
              this.router.navigate(['/listaSeguro']);
            },
            error: (error) => {
              alert('Error al editar asegurado, revise si código ya esta creado');
            }
          });
      } else {
        console.log(this.seguroForm.value);
        this.servSeguros.saveSeguro(this.seguroForm.value)
          .subscribe({
            next: (response) => {
              this.router.navigate(['/listaSeguro']);
            },
            error: (error) => {
              alert('Error al guardar asegurado,, revise si código ya esta creado');
            }
          }
          );
      }
    }
    else alert('Formulario Invalido. Datos Faltantes o erroneos');
  }
  validaNumero(even: any) {
    const dato = this.seguroForm.value;
    this.banNummeros = !dato.codigo.split('').every((caracter: string) => !isNaN(Number(caracter)));
  }

}

