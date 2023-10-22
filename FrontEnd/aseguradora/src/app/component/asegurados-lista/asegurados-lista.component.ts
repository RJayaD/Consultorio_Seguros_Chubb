import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AseguradosService } from 'src/app/services/asegurados.service';


@Component({
  selector: 'app-asegurados-lista',
  templateUrl: './asegurados-lista.component.html',
  styleUrls: ['./asegurados-lista.component.css']
})
export class AseguradosListaComponent implements OnInit {

  asegurados:any[]=[];
  private servAsegurados: AseguradosService;
  constructor(private router:Router, private serAsegurado:AseguradosService) {
    //this.asegurados=[];
    this.servAsegurados=serAsegurado;

  }

  ngOnInit(): void {
    this.servAsegurados.getAsegurados().subscribe({
      next: (response) => {
        if(response)
        this.asegurados = response;
      }
    }
    );

  }

  editarAsegurado(asegurado:any){
    this.router.navigate(['/Asegurado/'+asegurado.aseguradoId]);

  }

  eliminarAsegurado(asegurado:any){
    var Id=  asegurado.aseguradoId;
   this.servAsegurados.deleteAsegurado(Id).subscribe({
    next: (result) => {
      window.location.reload();
    },
    error: (error) => {
      console.error('Error al eliminar asegurado: ', error);
    }
  });

  }

  nuevoAsegurado(){
    this.router.navigate(['/Asegurado']);
  }

  masivoAsegurado(){
    this.router.navigate(['/masivoAsegurado']);
  }

}
