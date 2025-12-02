import { Routes } from '@angular/router';
import { Cars } from './cars/cars';
import { Registration } from './registration/registration';

export const routes: Routes = [
      { path: '', component: Cars },
      { path: 'registration', component: Registration }
];
