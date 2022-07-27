import React, { Component } from 'react';
import { Link } from "react-router-dom";


export class UseNasa extends Component {
    static displayName = UseNasa.name;

    constructor(props) {
        super(props);
        this.state = { nasaImages: [], loading: true };
    }

    componentDidMount() {
        this.getNasaImages();

    }
    componentDidUpdate() {
        this.getNasaImages();
    }
    
    static renderNasaTable(nasaImages) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Title</th>
                        <th>Description</th>
                        <th>DateCreated</th>
                        <th>Href</th>
                    </tr>
                </thead>
                <tbody>
                    {nasaImages.map(image =>
                        <tr key={image.title}>
                            <td>{image.title}</td>
                            <td>{image.description}</td>
                            <td>{image.dateCreated}</td>
                            <a href={image.href}>
                                <img src={image.href} width={250} height={250} alt={image.title} />
                            </a>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : UseNasa.renderNasaTable(this.state.nasaImages);

        return (
            <div>
                <h1 id="tabelLabel">Nasa list</h1>
                <p>This component demonstrates fetching Nasa from Another API.</p>
                {contents}
                <br/>
                <button className="btn btn-primary" onClick={() => this.addNasa()}>Add Nasa</button>
            </div>
        );
    }

    async getNasaImages() {
        const response = await fetch('api/nasa');
        const data = await response.json();
        this.setState({ nasaImages: data, loading: false });
    }

    async addNasa() {
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                    "id": "button_id",
                    "name": "button_name"
                })
        };
        await fetch('api/nasa', requestOptions);
    }
}