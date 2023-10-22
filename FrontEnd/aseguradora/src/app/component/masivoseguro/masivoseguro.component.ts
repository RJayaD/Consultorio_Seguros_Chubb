import { Component, OnInit } from '@angular/core';
import { TxtService } from 'src/app/services/txt.services.service';

@Component({
  selector: 'app-masivoseguro',
  templateUrl: './masivoseguro.component.html',
  styleUrls: ['./masivoseguro.component.css']
})
export class MasivoseguroComponent implements OnInit {

  seguros: any[]=[];
  public importedData: Array<any> = [];
  constructor(private _txtService: TxtService) {

  }

  ngOnInit(): void {
  }

  public async importDataFromTxt(event: any) {
    let fileContent = await this.getTextFromFile(event);
    console.log(fileContent);
    this.importedData = this._txtService.importDataFromTxt(fileContent);
    this.seguros=this.importedData;
    console.log(this.seguros);
  }

  private async getTextFromFile(event: any) {
    const file: File = event.target.files[0];
    let fileContent = await file.text();
    return fileContent;
  }
  limpiar(){
    this.seguros= [];
  }

}
