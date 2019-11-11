import React from 'react';
import { render } from 'react-dom';
import App from './App'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'jquery/dist/jquery.min.js'
import 'bootstrap/dist/js/bootstrap.min.js'
import './style/style.css';

render(<App />, document.querySelector('#root'));
