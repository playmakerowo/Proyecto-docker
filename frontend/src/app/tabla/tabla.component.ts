import { Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { Users } from '../models/Users.model';
import { ApiControllerService } from '../service/api-controller.service';

@Component({
  selector: 'app-tabla',
  standalone: true,
  imports: [RouterOutlet, TablaComponent, RouterLink, RouterLinkActive],
  templateUrl: './tabla.component.html',
  styleUrl: './tabla.component.css'
})
export class TablaComponent implements OnInit{
  Users: Users[] = [];

  constructor(private _usuarios: ApiControllerService){

  }
  
  ngOnInit(): void {
    this._usuarios.getUsers().subscribe((data: Users[]) => {this.Users = data
    console.log(data)
    })
  }
}
