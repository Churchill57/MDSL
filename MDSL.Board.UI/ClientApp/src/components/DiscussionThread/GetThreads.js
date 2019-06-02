import React, { Component } from 'react';
import authService from '../api-authorization/AuthorizeService'

export class GetThreads extends Component {
    static displayName = GetThreads.name;

  constructor(props) {
    super(props);
    this.state = { threads: [], loading: true };
  }

  componentDidMount() {
      this.populateThreadsDataLocal();
      // TODO: Get populateThreadsDataWebApi working!
      //this.populateThreadsDataWebApi();
  }

  static renderThreadsTable(threads) {
    return (
      <table className='table table-striped'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Title</th>
            <th>Body</th>
            <th>Created</th>
            <th>Created By</th>
          </tr>
        </thead>
        <tbody>
          {threads.map(thread =>
            <tr key={thread.id}>
                <td>{thread.id}</td>
                <td>{thread.title}</td>
                <td>{thread.body}</td>
                <td>{thread.created}</td>
                <td>{thread.createdBy}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : GetThreads.renderThreadsTable(this.state.threads);

    return (
      <div>
        <h1>Discussion Threads</h1>
        <p>This component demonstrates fetching thread data from the server.</p>
        {contents}
      </div>
    );
  }

    // TODO: Figure out failure to fetch err -2147418113 TypeError
    async populateThreadsDataWebApi() {
        const token = await authService.getAccessToken();
        const response = await fetch('http://localhost:44400/GetThreadsAsync', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ threads: data, loading: false });
    }

    async populateThreadsDataLocal() {
        const token = await authService.getAccessToken();
        const response = await fetch('api/DiscussionBoard/GetThreads', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ threads: data, loading: false });
    }
}
