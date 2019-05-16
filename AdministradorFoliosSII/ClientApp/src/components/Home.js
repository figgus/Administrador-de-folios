import React, { Component } from 'react';

export class Home extends Component {
    displayName = Home.name

    constructor() {
        super();
        this.state = {
            primero: 0,
            ultimo: 0,
            restantes: 0,
            ultimoFolioVendido:0,
        }
    }

    CambiarPrimero() {
        var primerFolio = document.getElementById('primero').value;
        fetch('http://localhost:49929/setPrimerFolio?valor=' + primerFolio, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },

        }).then(function (response) {
            if (response.ok) {
                alert('listo');
            }
            else {
                alert('algo salio mal');
            }
            });
        this.setState({ primero: primerFolio, ultimoFolioVendido: primerFolio });
    }

    CambiarUltimo() {
        var segundoFolio = document.getElementById('ultimo').value;
        fetch('http://localhost:49929/setUltimoFolio?valor=' + segundoFolio, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },

        }).then(function (response) {
            if (response.ok) {
                alert('listo');
            }
            else {
                alert('algo salio mal');
            }
            });;
        const total = segundoFolio - this.state.primero ;
        this.setState({ ultimo: segundoFolio, restantes: total });
    }

    RealizarVenta() {
        fetch('http://localhost:49929/RealizarVenta', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },

        })
        const restantes = this.state.restantes - 1;
        if (restantes < 0) {
            alert('no quedan folios disponibles');
        }
        else {
            var proximoFolio = Number(this.state.ultimoFolioVendido) + 1;
            this.setState({ restantes: restantes, ultimoFolioVendido: proximoFolio });
        }
        }

    NuevoSetFolios() {
        var desde = document.getElementById('primero').value;
        var hasta =document.getElementById('ultimo').value;
        fetch('http://localhost:49929/api/InsersionFolios', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                numFolioDesde: desde,
                numFolioHasta: hasta,
            })
        })
    }

  render() {
    return (
        <div>
            <p>Desde <input id="primero" type="number" />  <button onClick={() => { this.CambiarPrimero() }}  > Ingresar </button> </p>
            <p>Hasta <input id="ultimo" type="number" />  <button onClick={() => { this.CambiarUltimo(), this.NuevoSetFolios() }} >Ingresar  </button>  </p>
            <p> {this.state.restantes} </p>
            <button onClick={() => { this.RealizarVenta() }}>Realizar venta </button>
            <p>La ultima venta tiene el folio:  {this.state.ultimoFolioVendido}</p>
      </div>
    );
  }
}
