import { Component, OnInit } from '@angular/core';
import { ApiControllerService } from '../service/api-controller.service';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-modificar',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './modificar.component.html',
  styleUrl: './modificar.component.css'
})
export class ModificarComponent implements OnInit {
  id = 0;

  constructor(
    private _usuarios: ApiControllerService,
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      this.id = params['id'];
    });
  }
}
