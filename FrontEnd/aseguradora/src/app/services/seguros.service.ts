import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SegurosService {
  urlLocal: string = 'https://localhost';
  port: string =':7031';
  context: string = '/api';

  constructor(private http: HttpClient) { }

  getseguros(): Observable<any>{
    return this.http.get<any>(this.urlLocal + this.port+this.context+'/Seguro/verseguros');
  }

  editSeguro(seguro:any):Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(this.urlLocal + this.port+this.context+'/Seguro/actualizarSeguro', seguro, { headers });
  }
  saveSeguro(seguro:any):Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(this.urlLocal + this.port+this.context+'/Seguro/guardarSeguro', seguro, { headers });
  }

  buscarSeguroId(id:any):Observable<any>{
    return this.http.get<any>(this.urlLocal + this.port+this.context+'/Seguro/buscarSeguroId/?id='+id);

  }
  buscarSeguroCondicion(condicion:any):Observable<any>{
    return this.http.get<any>(this.urlLocal + this.port+this.context+'/Seguro/verseguroxcondicion/?condicion='+condicion);

  }
  deleteSeguro(seguroId:any):Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(this.urlLocal + this.port+this.context+'/Seguro/eliminarSeguro/?id='+seguroId, seguroId, { headers } );

  }
}
