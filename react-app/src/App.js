import React, { Component } from 'react';
import NavigationBar from './components/NavigationBar'
import { BrowserRouter, Route } from 'react-router-dom'
import Machine from './components/Machine'
import Product from './components/Product'
import MachineType from './components/MachineType'

class App extends Component {

  render() {
    return (
      <div>
        <NavigationBar />
        <div class="center">
          <div className="content">
            <Machine/>
            <MachineType/>
            <Product/>
          </div>
        </div>
      </div>
    )
  }

  /*
        <div className="content">
          <Machine/>
        </div>
  */
  //Before last commit
  // render() {
  //   return (
  //     <div>
  //       <h1>MyOwnCutlery</h1>
  //       <hr/>
  //       <div className="content">
  //         <Machine/>
  //       </div>

  //       <div className="content">
  //         <MachineTypes machineTypes={this.state.machineTypes} />
  //       </div>
  //     </div>

  //   )
  // }

}

export default App;