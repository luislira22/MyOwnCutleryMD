import React, {Component} from 'react';
import Products from './components/Product.js'
import MachineTypes from './components/MachineType.js';

class App extends Component {
    state = {
      products: [],
      machineTypes: []
    }

    componentDidMount() {

      fetch('https://localhost:5001/api/products')
      .then(results => results.json())
      .then((data) => {
          this.setState({products : data})
      })
      .catch(console.log);
      
      fetch('https://localhost:5001/api/machineType')
      .then(results => results.json())
      .then((data) => {
          this.setState({machineTypes : data})
          console.log(data);
      })
      .catch(console.log);
  }

  render() {
    return (
      <div>
        <h1>MyOwnCutlery</h1>
        <hr/>
        <div className="content">
          <Products products={this.state.products} />
        </div>
        <div className="content">
          <MachineTypes machineTypes={this.state.machineTypes} />
        </div>
      </div>
      
    )
  }
}

export default App;