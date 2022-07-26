import React, { Component } from 'react';

export class UseClients extends Component {
    static displayName = UseClients.name;

    constructor(props) {
        super(props);
        this.state = { clients: [], loading: true };
    }

    componentDidMount() {
        this.populateClients();
    }

    static renderClientsTable(clients) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                    </tr>
                </thead>
                <tbody>
                    {clients.map(client =>
                        <tr key={client.id}>
                            <td>{client.id}</td>
                            <td>{client.name}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : UseClients.renderClientsTable(this.state.clients);

        return (
            <div>
                <h1 id="tabelLabel">Clients list</h1>
                <p>This component demonstrates fetching Clients from Another API.</p>
                {contents}
            </div>
        );
    }

    async populateClients() {
        const response = await fetch('api/apiactions');
        const data = await response.json();
        this.setState({ clients: data, loading: false });
    }
}