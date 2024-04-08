import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Users } from '../models/Users.model';
import { ApiControllerService } from '../service/api-controller.service';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2'

@Component({
  selector: 'app-detalle',
  standalone: true,
  imports: [RouterLink, CommonModule],
  templateUrl: './detalle.component.html',
  styleUrl: './detalle.component.css'
})
export class DetalleComponent implements OnInit {
  User?: Users;
  id = 0;
  Name = "";
  Age = 0;


  constructor(
    private _usuarios: ApiControllerService,
    private _route: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this._route.params.subscribe(params => {
      this.id = params['id'];
    });

    this._usuarios.getUser(this.id).subscribe((data: Users) => {
      this.User = data;
      this.Name = this.User?.name
      this.Age = this.User?.age
      console.log(this.User)
    })
  }

  borrarUsuario() {
    Swal.fire({
      // Se solicita confirmación
      title: '¿Estás seguro?',
      text: '¡No podrás deshacer esta acción!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Sí, eliminar',
      cancelButtonText: 'Cancelar'
    }).then((result) => {
      if (result.isConfirmed) {
        // Se consume el API de eliminación
        this._usuarios.removeUser(this.id).subscribe(
          (data: Users) => {
            Swal.fire({
              title: 'Eliminado',
              text: 'El usuario ha sido eliminado.',
              icon: 'success',
              showConfirmButton: false,
              html: '<a class="w3-button w3-round-large w3-indigo w3-hover-blue" href="/tabla">Volver a tabla</a>'
            });
          },
          (error) => {
            console.error('Error al eliminar usuario:', error);
            Swal.fire({
              title: 'Error',
              text: 'Ocurrió un error al intentar eliminar el usuario.',
              icon: 'error',
              confirmButtonText: 'OK'
            });
          }
        );
      }
    });
  }
  
}
