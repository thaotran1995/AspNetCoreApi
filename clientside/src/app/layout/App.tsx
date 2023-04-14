import axios from 'axios';
import React, { useEffect, useState} from 'react';
import { Owner } from './models/o/owner';

function App() {
  const [owners, setOwners] = useState<Owner[]>([]);
  useEffect(() => {
    axios.get("/api/owner")
    .then(response => {
      console.log(response);
      setOwners(response.data);
    })
  }, [])
  return (
    <div className="App">
      <header className="App-header">
        <h1>List Owners</h1>
        {owners?.map(item => (
          <div key={item.id}>
            <p>{item.name}</p>
            <ul>
              <li>{item.dateOfBirth}</li>
              <li>{item.address}</li>
            </ul>
          </div>
        ))}
      </header>
    </div>
  );
}

export default App;
