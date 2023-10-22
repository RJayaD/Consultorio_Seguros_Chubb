import { Component, OnInit } from '@angular/core';
import { Router} from '@angular/router';
import { SegurosService } from 'src/app/services/seguros.service';


@Component({
  selector: 'app-seguros-lista',
  templateUrl: './seguros-lista.component.html',
  styleUrls: ['./seguros-lista.component.css']
})
export class SegurosListaComponent implements OnInit{

  seguros: any[];
  private servSeguros: SegurosService;

  constructor(private router: Router, servSeguros: SegurosService) {
    this.seguros = [];
    this.servSeguros = servSeguros;
  }

  ngOnInit(): void {
    this.servSeguros.getseguros().subscribe({
      next: (response) => {
        this.seguros = response;
      }
    }
    );
  }
  editarSeguro(seguro: any) {
     this.router.navigate(['/Seguro/'+seguro.seguroId]);
  }

  eliminarSeguro(seguro: any) {
   var Id=  seguro.seguroId 
   this.servSeguros.deleteSeguro(Id).subscribe({
    next: (result) => {
      window.location.reload();
    },
    error: (error) => {
      console.error('Error al eliminar seguro: ', error);
    }
  });
  }
  nuevoSeguro() {
    this.router.navigate(['/Seguro']);
  }
  masivoSeguro() {
    this.router.navigate(['/masivoSeguro']);
  }

}
