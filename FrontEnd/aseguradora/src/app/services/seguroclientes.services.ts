import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
  })
  export class SeguroClientesService {
    urlLocal: string = 'https://localhost';
    port: string =':7031';
    context: string = '/api';
  
    constructor(private http: HttpClient) { }
  
    getSeguroClientes(): Observable<any>{
      return this.http.get<any>(this.urlLocal + this.port+this.context+'/SeguroCliente/verseguroclientes');
    }

    getSeguroClientesxCondicional(condicional:any): Observable<any>{
        return this.http.get<any>(this.urlLocal + this.port+this.context+'/SeguroCliente/verseguroclientesxcondicional/?condicional='+condicional);
      }
    
    getSeguroClientesxCodigo(codigo:any): Observable<any>{
        return this.http.get<any>(this.urlLocal + this.port+this.context+'/SeguroCliente/verseguroclientesxcodigo/?codigo='+codigo);
      }

    saveSeguroCliente(segurocliente:any):Observable<any>{
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      return this.http.post<any>(this.urlLocal + this.port+this.context+'/SeguroCliente/AgregarSeguroCliente', segurocliente, { headers });
    }
    deleteSeguroCliente(Id:any):Observable<any>{
      const headers = new HttpHeaders({
        'Content-Type': 'application/json'
      });
      return this.http.post<any>(this.urlLocal + this.port+this.context+'/SeguroCliente/DeleteAsegurado/?id='+Id, Id, { headers } );
  
    }
  }