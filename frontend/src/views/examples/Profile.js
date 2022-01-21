import {
  Button,
  Card,
  CardHeader,
  CardBody,
  Container,
  Row,
  Col,
} from "reactstrap";
// core components
import UserHeader from "components/Headers/UserHeader.js";

import { useState, useEffect } from 'react'

import useToken from '../../useToken'

const getStudentCount = async (schoolId, serviceId, token) => {
    const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School/' + schoolId + '/Service/' + serviceId, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    })

    const body = await result.json()
    return body.data.students.length
}

const getSchool = async (token) => {
    const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'Bearer ' + token
        }
    })

    const body = await result.json()
    //console.log("body? ", body)
    return body.data[0]
}

const Profile = () => {
    const [ serviceLength, setServiceLength ] = useState(0);
    const [ schoolName, setSchoolName ] = useState("");
    const [ name, setName ] = useState('')
    const [ studentCount, setStudentCount ] = useState(0);
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
            //console.log("body in admin navbar", body)
            setName(body.data.name + ' ' + body.data.surName)

            const school = await getSchool(token)
            const serviceLength = school.services.length
            const schoolName = school.name
            const allServices = school.services
            let allStudents = 0;

            // loop through the services and push the data into the array
            for (let i = 0; i < allServices.length; i++) {
                const id = allServices[i].id
                allStudents += await getStudentCount(school.id, id, token)
            }

            setServiceLength(serviceLength)
            setSchoolName(schoolName)
            setStudentCount(allStudents)
        }

        fetchData()
    }, [])

  return (
    <>
      <UserHeader />
      {/* Page content */}
      <Container className="mt--7" fluid>
        <Row>
          <Col className="order-xl-2 mb-5 mb-xl-0" xl="4">
            <Card className="card-profile shadow">
              <Row className="justify-content-center">
                <Col className="order-lg-2" lg="3">
                  <div className="card-profile-image">
                    <a href="#pablo" onClick={(e) => e.preventDefault()}>
                      <img
                        alt="..."
                        className="rounded-circle"
                        src={
                          require("../../assets/img/theme/profile.jpg")
                            .default
                        }
                      />
                    </a>
                  </div>
                </Col>
              </Row>
              <CardHeader className="text-center border-0 pt-8 pt-md-4 pb-0 pb-md-4">
                <div className="d-flex justify-content-between">
                  <Button
                    className="mr-4"
                    color="info"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                    size="sm"
                  >
                    SSS
                  </Button>
                  <Button
                    className="float-right"
                    color="default"
                    href="#pablo"
                    onClick={(e) => e.preventDefault()}
                    size="sm"
                  >
                    SSS
                  </Button>
                </div>
              </CardHeader>
              <CardBody className="pt-0 pt-md-4">
                <Row>
                  <div className="col">
                    <div className="card-profile-stats d-flex justify-content-center mt-md-5">
                      <div>
                        <span className="heading"> {serviceLength} </span>
                        <span className="description">Servisler</span>
                      </div>
                      <div>
                        <span className="heading"> {serviceLength} </span>
                        <span className="description">Sürücüler</span>
                      </div>
                      <div>
                        <span className="heading">{ studentCount }</span>
                        <span className="description">Öğrenciler</span>
                      </div>
                      
                    </div>
                  </div>
                </Row>
                <div className="text-center">
                  <h3>
                    {name}
                  </h3>

                  <div className="h5 mt-4">
                    <i className="ni business_briefcase-24 mr-2" />
                    Manager
                  </div>
                  <div>
                    <i className="ni education_hat mr-2" />
                    {schoolName}
                  </div>
                  <hr className="my-4" />
                  
                </div>
              </CardBody>
            </Card>
          </Col>
          
        </Row>
      </Container>
    </>
  );
};

export default Profile;
