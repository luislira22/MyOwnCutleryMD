import React, {Component} from 'react';
import loading from '../img/loading.gif'

class Loading extends Component {
    render () {
        return (
            <div style={{textAlign: "center"}}>
                <img src={loading} alt="Loading..." height="150" width="150"/>
            </div>
        )
    }
}

export default Loading;