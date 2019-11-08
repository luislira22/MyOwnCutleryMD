import React, { Component } from 'react';
import NavigationBar from './NavigationBar'
import Machine from './components/Machine'
import Product from './components/Product'
import MachineType from './components/MachineType'
import Operation from './components/Operation'

function App(){
    return (
      <div>
        <NavigationBar />
        <div class="center">
          <div className="content">
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

export default App;