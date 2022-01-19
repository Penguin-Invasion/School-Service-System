
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

    const [userName, setUserName] = useState('');
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [lastName, setLastName] = useState('');

    function handleSubmit(event) {
      event.preventDefault();
      console.log('name:', name);
      console.log('email:', email);
      console.log('lastName:', lastName);
      console.log('userName:', userName);


      editProf({
       "id"  : Math.random(),
       "name":name,
       "email":email,
       "lastName":lastName,
       "userName:": userName
     });
    }

    async function editProf(credentials) {
        return fetch('http://localhost:3001/users', {
          method: 'post',//patch yapilip degere gore yapilcak not know clearly so post yaptim
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

                <CardBody>
                <form >
                    <div className="mb-3" >
                        <label htmlFor="userName">Kullanici Adi</label>
                        <input
                        className="form-control"
                        id="userName"
                        type="text"
                        value={userName}
                        onChange={(e) => setUserName(e.target.value)}
                        />
                    </div>

                    <div className="mb-3" >
                        <label htmlFor="name">Isim</label>
                        <input
                        className="form-control"
                        id="name"
                        type="text"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        />
                    </div>

                    <div className="mb-3" >
                        <label htmlFor="lastName">Soyism</label>
                        <input
                        className="form-control"
                        id="lastName"
                        type="text"
                        value={lastName}
                        onChange={(e) => setLastName(e.target.value)}
                        />
                    </div>
                    <div className="mb-3" >
                        <label htmlFor="email">E-Mail</label>
                        <textarea
                        className="form-control"
                        id="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>
                    <div>
                        <button type="submit" onClick={handleSubmit}   >Submit</button>
                    </div>

                </form>
                </CardBody>
              </Card>
            </Col>
          </Row>
        </Container>
      </>
    );
  };

  export default Profile;
