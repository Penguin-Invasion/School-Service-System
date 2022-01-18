import { Link } from "react-router-dom";
import { Button, Container, Row, Col } from "reactstrap";

import { useState, useEffect } from 'react'

import useToken from '../../useToken'

const UserHeader = () => {

    const [ name, setName ] = useState('')
    const { token } = useToken();

    // fetch data from the api
    useEffect(() => {
        const fetchData = async () => {
            const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/Auth', {
                method: 'GET',
                headers: {
                    // set token
                    'Authorization': 'Bearer ' + token,
                    'Content-Type': 'application/json'
                }
            })
            const body = await result.json()
            setName(body.data.name + ' ' + body.data.surName)

        }

        fetchData()
    }, [])





  return (
    <>
      <div
        className="header pb-8 pt-5 pt-lg-8 d-flex align-items-center"
        style={{
          minHeight: "600px",
          backgroundImage:
            "url(" +
            require("../../assets/img/theme/profile-cover.jpg").default +
            ")",
          backgroundSize: "cover",
          backgroundPosition: "center top",
        }}
      >
        {/* Mask */}
        <span className="mask bg-gradient-default opacity-8" />
        {/* Header container */}
        <Container className="d-flex align-items-center" fluid>
          <Row>
            <Col lg="7" md="10">
              <h1 className="display-2 text-white">Merhaba {name}</h1>
              <p className="text-white mt-0 mb-5">
                This is your profile page. You can see your profile info and you can edit it. You can also see your image and can change it.
              </p>
              <div className="all-buttons">
                <Link
                    color="white"
                    //onClick={(e) => e.preventDefault()}
                    to="/admin/edit-profile"
                >
                <Button className="edit-profile">
                    Edit profile
                </Button>
                </Link>
                <Link
                    color="white"
                    to="/admin/add-driver"
                >
                <Button className="add-driver" >
                    Sürücü Ekle
                </Button>
                </Link>
                <Link
                    color="white"
                    //onClick={(e) => e.preventDefault()}
                    to="/admin/add-service"
                >
                <Button className="add-service">
                    Servis Ekle
                </Button>
                </Link>
              </div>
            </Col>
          </Row>
        </Container>
      </div>
    </>
  );
};

export default UserHeader;
