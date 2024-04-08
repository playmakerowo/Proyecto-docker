import { Routes } from '@angular/router';
import { TablaComponent } from './tabla/tabla.component';
import { HomeComponent } from './home/home.component';
import { DetalleComponent } from './detalle/detalle.component';
import { ModificarComponent } from './modificar/modificar.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'tabla', component: TablaComponent },
    { path: 'detalle/:id', component: DetalleComponent },
    { path: 'modificar/:id', component: ModificarComponent },
    { path: '**', component: HomeComponent }
];
