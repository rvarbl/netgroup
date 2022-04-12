import 'jquery';
import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'font-awesome/css/font-awesome.min.css';

import { StoreConfiguration } from '@aurelia/store-v1';
import Aurelia, { RouterConfiguration } from 'aurelia';
import { MyApp } from './my-app';
import { initialState } from './initialstate'; 

Aurelia
  .register(RouterConfiguration)
  .register(
    StoreConfiguration.withInitialState(initialState)
  )
  .app(MyApp)
  .start();
