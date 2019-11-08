import React, { Component } from 'react';
import NavigationBar from './components/NavigationBar'
import Machine from './components/Machine'
import Product from './components/Product'
import MachineType from './components/MachineType'
import SceneManager from './three/SceneManager'

class App extends Component {

  componentDidMount() {
    SceneManager(this.mount);
  }

  render() {
    return (
      <div>
        <NavigationBar />
        <div className="center">
          <div className="content">
            <Machine />
            <MachineType />
            <div ref={ref => (this.mount = ref)} />
          </div>
        </div>
      </div>
    )
  }
  //Before last commit
  // render() {
  //   return (
  //     <div>
  //       <h1>MyOwnCutlery</h1>
  //       <hr/>
  //       <div className="content">
  //         <Machine/>
  //       </div>

  componentWillUnmount() {
    //this.mount.removeChild()
  }
}

export default App;