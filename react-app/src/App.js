import React, {Component} from 'react';
import Products from './components/Product.js'

class App extends Component {
    state = {
      products: []
    }

    componentDidMount() {

      fetch('https://localhost:5001/api/product')
      .then(results => results.json())
      .then((data) => {
          this.setState({products : data})
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
      </div>
      
    )
  }
}

export default App;