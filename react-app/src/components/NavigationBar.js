import React, { useState } from 'react';
import { Navbar, Nav, Form, FormControl, Button } from 'react-bootstrap';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import MachineTypes from './MachineType';
import Machines from './Machine';
import Machines2 from './Machine2';
import Operations from './Operation';
import Products from './Product';
import style from '../style/style.css'
import { Scene } from 'three';
import SceneManager from '../three/SceneManager';

function NavigationBar(props) {
  return (
    <Router>
      <div>
        <Navbar bg="dark" variant="dark">
          <Navbar.Brand href="#home">My Own Cutlery</Navbar.Brand>
          <Nav className="mr-auto">
            <Nav.Link href="/machines">Machines</Nav.Link>
            <Nav.Link href="/machineTypes">MachineTypes</Nav.Link>
            <Nav.Link href="/products">Products</Nav.Link>
            <Nav.Link href="/operations">Operations</Nav.Link>
            <Nav.Link href="/visualization">Visualization</Nav.Link>
            <Nav.Link href="/machines2">Machines2</Nav.Link>
          </Nav>
          <Form inline>
            <FormControl type="text" placeholder="Search" className="mr-sm-2" />
            <Button variant="outline-info">Search</Button>
          </Form>
        </Navbar>
        <hr />
        <Switch>
          <div class="center">
            <Route path='/machines' component={Machines} />
            <Route path='/machineTypes' component={MachineTypes} />
            <Route path='/operations' component={Operations} />
            <Route path='/products' component={Products} />
            <Route path='/visualization' component={SceneManager} />
            <Route path='/machines2' component={Machines2} />
          </div>
        </Switch>
      </div>
    </Router>
  );
}

export default NavigationBar;