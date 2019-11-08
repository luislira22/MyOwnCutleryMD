import React, { Component } from 'react';
import Navbar from 'react-bootstrap/Navbar';
import Nav from 'react-bootstrap/Nav';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import FormControl from 'react-bootstrap/FormControl';
import { BrowserRouter, Route } from 'react-router-dom'
import Machine from './Machine'
import MachineType from './MachineType'
import { LinkContainer, Link } from "react-router-bootstrap";
import { NavItem } from "react-bootstrap";
import {Routes} from "react-router-bootstrap";

class NavigationBar extends Component {

  render() {
    return (
      <Navbar bg="dark" variant="dark">
        <Navbar.Brand href="#home">My Own Cutlery</Navbar.Brand>
        <Nav className="mr-auto">
          <Nav.Link href="/machine">Machines</Nav.Link>
          <Nav.Link href="/machinetypes">MachineTypes</Nav.Link>
          <Nav.Link href="/products">Products</Nav.Link>
        </Nav>
        <Form inline>
          <FormControl type="text" placeholder="Search" className="mr-sm-2" />
          <Button variant="outline-info">Search</Button>
        </Form>
      </Navbar>
      
    );
  }
}

export default NavigationBar;