import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { CarroResponse } from '../models/carro-response';

@Injectable()
export class CarroService {

    constructor(private http: HttpClient) { }

    baseUrl = `${environment.apiUrl}/api/Carro`

    public async obterCarros(): Promise<Array<CarroResponse>> {
        try {
            const url = this.baseUrl + '/obter-carros'
            const request = {
                "marca": null,
                "modelo": null,
                "potenciaMin": null,
                "potenciaMax": null,
                "torqueMin": null,
                "torqueMax": null,
                "combustivel": null,
                "precoDiariaMin": null,
                "precoDiariaMax": null,
                "anoMin": null,
                "anoMax": null
              }
            return this.http.post<Array<CarroResponse>>(url, request).toPromise();
        } catch (error) {
            throw error;
        }
    }

}
