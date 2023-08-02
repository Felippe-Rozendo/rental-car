import { Component, OnInit } from '@angular/core';
import { CarroResponse } from 'src/app/models/carro-response';
import { CarroService } from 'src/app/services/carro.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private service: CarroService) { }

  ngOnInit() {
    this.obterListaCarros()
  }
  url = environment.apiUrl;
  catalogoCarros: Array<CarroResponse>;

  async obterListaCarros(){
    this.catalogoCarros = await this.service.obterCarros();
  }


  imagemUrl(imagem: string){
    return `${this.url}/${imagem.replace("\\", "/")}`;
  }

}
