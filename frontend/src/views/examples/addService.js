
import { Link } from "react-router-dom";
import {
    Button,
    Card,
    CardHeader,
    CardBody,
    FormGroup,
    Form,
    Input,
    Container,
    Row,
    Col,
  } from "reactstrap";
  // core components
  import UserHeader from "components/Headers/UserHeader.js";
  import { useState, useEffect } from "react";

  const Profile = () => {
    const [user, setUser] = useState([]);
    const [services, setServices] = useState([]);
    const [service, setService] = useState([]);

    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [message, setMessage] = useState('');

  function handleSubmit(event) {
    event.preventDefault();
    console.log('name:', name);
    console.log('message:', message);
    createService({
      "id":Math.random(),
     "name":name,
     "message":message
   });
  }

  async function createService(credentials) {
      return fetch('http://localhost:3001/services', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(credentials)
      })
        .then(data => data.json())
  }



    return (
      <>
        <UserHeader />
        {/* Page content */}
        <Container className="mt--7  " fluid>
          <Row>
            <Col className="order-xl-1" xl="8">
              <Card className="bg-secondary shadow">
                <form >
                    <div className="mb-3">
                        <label htmlFor="name">Servis Plakasi</label>
                        <input
                        className="form-control"
                        id="name"
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        />
                    </div>
                    <div className="mb-3">
                        <label htmlFor="message">Message</label>
                        <textarea
                        className="form-control"
                        id="message"
                        value={message}
                        onChange={(e) => setMessage(e.target.value)}
                        />
                    </div>
                    <button type="submit" onClick={handleSubmit}>Submit</button>
                </form>
              </Card>
            </Col>
          </Row>
        </Container>
      </>
    );
  };

  export default Profile;
