import React, { useState } from 'react';
import { Navbar, Nav, NavLink, NavItem, NavDropdown, MenuItem,Form,FormControl,Button } from 'react-bootstrap';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import MachineTypes from './components/MachineType';
import Machines from './components/Machine';
import Operations from './components/Operation';
import Products from './components/Product';

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
          </Nav>
          <Form inline>
            <FormControl type="text" placeholder="Search" className="mr-sm-2" />
            <Button variant="outline-info">Search</Button>
          </Form>
        </Navbar>
        <hr />
        <Switch>
          <Route path='/machines' component={Machines} />
          <Route path='/machineTypes' component={MachineTypes} />
          <Route path='/operations' component={Operations} />
          <Route path='/products' component={Products} />
        </Switch>
      </div>
    </Router>
  );
}

export default NavigationBar;