export class Asegurado {
    cedula:string;
    nombre_cliente: string;
    fecha_creacion: Date;
    telefono: string;
    edad: number;
  constructor() {
    this.cedula = '';
    this.nombre_cliente = '';
    this.fecha_creacion= new Date();
    this.telefono = '';
    this.edad = 0;
  }
  }