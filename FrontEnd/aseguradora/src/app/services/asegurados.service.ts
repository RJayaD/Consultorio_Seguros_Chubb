import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AseguradosService {
  urlLocal: string = 'https://localhost';
  port: string =':7031';
  context: string = '/api';

  constructor(private http: HttpClient) { }

  getAsegurados(): Observable<any>{
    return this.http.get<any>(this.urlLocal + this.port+this.context+'/Asegurado/verasegurados');
  }

  editAsegurado(asegurado:any):Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(this.urlLocal + this.port+this.context+'/Asegurado/actualizarAsegurado', asegurado, { headers });
  }
  buscarAseguradoId(id:any):Observable<any>{
    return this.http.get<any>(this.urlLocal + this.port+this.context+'/Asegurado/buscarAseguradoId/?id='+id);

  }
  buscarAseguradoCondicion(condicion:any):Observable<any>{
    return this.http.get<any>(this.urlLocal + this.port+this.context+'/Asegurado/veraseguradosxcondicion/?condicion='+condicion);

  }
  saveAsegurado(asegurado:any):Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(this.urlLocal + this.port+this.context+'/Asegurado/agregarAsegurado', asegurado, { headers });
  }
  saveAseguradosMasivamente(asegurado: any[]): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(this.urlLocal + this.port + this.context + '/Asegurado/agregarAseguradoMasivamente', asegurado, { headers });
  }
  deleteAsegurado(aseguradoId:any):Observable<any>{
    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    return this.http.post<any>(this.urlLocal + this.port+this.context+'/Asegurado/eliminarAsegurado/?id='+aseguradoId, aseguradoId, { headers } );

  }
}
