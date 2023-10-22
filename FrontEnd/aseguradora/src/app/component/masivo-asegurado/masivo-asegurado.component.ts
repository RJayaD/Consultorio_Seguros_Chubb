import { Component, OnInit } from '@angular/core';
import { TxtService } from 'src/app/services/txt.services.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AseguradosService } from 'src/app/services/asegurados.service';


@Component({
  selector: 'app-masivo-asegurado',
  templateUrl: './masivo-asegurado.component.html',
  styleUrls: ['./masivo-asegurado.component.css']
})
export class MasivoAseguradoComponent implements OnInit {
  asegurados: any[] = [];
  public importedData: Array<any> = [];

  constructor(private _txtService: TxtService,private servAsegurado:AseguradosService, private formBuilder: FormBuilder,private router: Router, private routeData: ActivatedRoute)
  {

  }

  ngOnInit(): void {
    
  }

  public async importDataFromTxt(event: any) {
    let fileContent = await this.getTextFromFile(event);
    console.log(fileContent);
    this.importedData = this._txtService.importDataFromTxt(fileContent);
    this.asegurados=this.importedData;
    console.log(this.asegurados);
  }

  private async getTextFromFile(event: any) {
    const file: File = event.target.files[0];
    let fileContent = await file.text();
    return fileContent;
  }

   Guardar(event: any){
    console.log(this.asegurados);
    this.servAsegurado.saveAseguradosMasivamente(this.asegurados).subscribe({
      next: (response) => {
        console.log('Asegurados guardados con éxito');
        this.router.navigate(['/listaAsegurado']);
      },
      error: (error) => {
        alert("Error al guardar asegurados masivamente, Cédula ya existe or valor fuera de rango");
      }
    });
  }


  limpiar(){
    this.asegurados= [];
  }
}
