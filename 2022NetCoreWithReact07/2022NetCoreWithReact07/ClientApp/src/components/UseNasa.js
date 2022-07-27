import React, { Component } from 'react';
import { Link } from "react-router-dom";


export class UseNasa extends Component {
    static displayName = UseNasa.name;

    constructor(props) {
        super(props);
        this.state = {
            nasaImages: [],
            loading: true,
            searchWord: "",
            startYear: "",
            endYear: "",
            imageType: "image"
        };
        this.handleChange = this.handleChange.bind(this);
    }
    
    handleChange(event) {
        this.setState({ value: event.target.value });
    }

    componentDidMount() {
        this.getNasaImages();

    }
    componentDidUpdate() {
        //this.getNasaImages();
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
                <br />
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                    <tr>
                        <th>search word</th>
                        <th>start year</th>
                        <th>end year</th>
                        <th>image type</th>
                    </tr>
                    </thead>
                    <tbody>
                        <tr key="start word">
                            <td><input type="text" onChange={e => this.setState({ searchWord: e.target.value })} /></td>
                            <td><input type="text" onChange={b => this.setState({ startYear: b.target.value })} /></td>
                            <td><input type="text" onChange={d => this.setState({ endYear: d.target.value })} /></td>
                            <td><input type="text" value={this.state.imageType} /></td>
                        </tr>
                    </tbody>
                </table>
                <br/>
                <button className="btn btn-primary" onClick={() => this.getNasaImages()}>Get Nasa Images </button>
                <br />
                {contents}
            </div>
        );
    }
    
    async getNasaImages() {
        const baseQuery = "api/nasa?";
        const fullQuery = baseQuery +
            "query=" +
            this.state.searchWord +
            "&startyear=" +
            this.state.startYear +
            "&endYear=" +
            this.state.endYear +
            "&mediaType=" +
            this.state.imageType;
        //baseQuery.push(this.state.startYear);
        const response = await fetch(fullQuery);
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