import React, { Component } from 'react';
import NavigationBar from './components/NavigationBar'
import Machine from './components/Machine'
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

  componentWillUnmount() {
    //this.mount.removeChild()
  }
}

export default App;