import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name
    constructor() {
        super();
        this.state = {
           
        }
    }

    

    render() {
        return (
            <div>
                <p>Primer folio <input id="primero" type="number" />  <button onClick={() => { this.CambiarPrimero() }}  > Ingresar </button> </p>
                <p>Ultimo folio <input id="ultimo" type="number" />  <button onClick={() => { this.CambiarUltimo() }} >Ingresar  </button>  </p>
                <p> {this.state.restantes} </p>
                <button onClick={() => { this.RealizarVenta() }}>Realizar venta </button>
                <p>Ultimo folio vendido {this.state.ultimoFolioVendido}</p>
            </div>
        );
    }
}
