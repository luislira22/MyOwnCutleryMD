import React, {Component} from 'react';
import Machine from './components/Machine'

class App extends Component {

  render() {
    return (
      <div>
        <h1>MyOwnCutlery</h1>
        <hr/>
        <div className="content">
          <Machine/>
        </div>
      </div>
      
    )
  }
}

export default App;