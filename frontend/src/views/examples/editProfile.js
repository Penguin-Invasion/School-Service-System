
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

  import useToken from '../../useToken'

  const Profile = () => {

    const [password, setPassword] = useState('');
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [lastName, setLastName] = useState('');

    const { token } = useToken();

    function handleSubmit(event) {
      event.preventDefault();
      console.log('name:', name);
      console.log('lastName:', lastName);
      console.log('email:', email);
      console.log('password:', password);

      // create a credentials object
    const credentials = {}
        // add the values to the credentials object
        // if values are not empty
        if (name) credentials.name = name;
        if (lastName) credentials.surName = lastName;
        if (email) credentials.email = email;
        if (password) credentials.password = password;

        console.log('credentials:', credentials);
      editProf(credentials);
    }

    async function editProf(credentials) {
        return fetch('https://schoolservicesystem.azurewebsites.net/api/Profile', {
          method: 'PATCH',
          headers: {
            // set token
            'Authorization': 'Bearer ' + token,
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
                <p>Bu ekrandan, bilgilerinizi güncelleyebilirsiniz. Sadece değişmesini istdiğiniz bilgileri gönderebilirsiniz.</p>
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
                    <div className="mb-3" >
                        <label htmlFor="userName">Yeni Şifre</label>
                        <input
                        className="form-control"
                        id="userName"
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        />
                    </div>

                    <div>
                       
                    <Button onClick={handleSubmit} className="add-service">
                        Gönder
                    </Button>
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
