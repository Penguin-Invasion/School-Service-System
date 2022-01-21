import { Card, CardBody, CardTitle, Container, Row, Col } from "reactstrap";
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


const Header = () => {
    const [serviceLength, setServiceLength ] = useState(0);
    const [ studentCount, setStudentCount ] = useState(0);
    const { token } = useToken();

    // fetch data from the api
    useEffect(() => {
        const fetchData = async () => {
            const result = await fetch('https://schoolservicesystem.azurewebsites.net/api/School', {
                method: 'GET',
                headers: {
                    // set token
                    'Authorization': 'Bearer ' + token,
                    'Content-Type': 'application/json'
                }
            })
            const body = await result.json()
            const schoolId = body.data[0].id
            const allServices = body.data[0].services
            let allStudents = 0;
            // loop through the services and push the data into the array
            for (let i = 0; i < allServices.length; i++) {
                const id = allServices[i].id

                allStudents += await getStudentCount(schoolId, id, token)
            }

            setServiceLength(body.data[0].services.length)
            setStudentCount(allStudents)
        }

        fetchData()
    }, [])


  return (
    <>
      <div className="header penguin-bg pb-8 pt-5 pt-md-8">
        <Container fluid>
          <div className="header-body">
            {/* Card stats */}
            <Row>
              <Col lg="6" xl="3">
                <Card className="card-stats mb-4 mb-xl-0">
                  <CardBody>
                    <Row>
                      <div className="col">
                        <CardTitle
                          tag="h5"
                          className="text-uppercase text-muted mb-0"
                        >
                          Servis Sayısı
                        </CardTitle>
                        <span className="h2 font-weight-bold mb-0">{serviceLength}</span>
                      </div>
                      <Col className="col-auto">
                        <div className="icon icon-shape bg-danger text-white rounded-circle shadow">
                          <i className="fas fa-chart-bar" />
                        </div>
                      </Col>
                      
                    </Row>
                  </CardBody>
                </Card>
              </Col>
              <Col lg="6" xl="3">
                <Card className="card-stats mb-4 mb-xl-0">
                  <CardBody>
                    <Row>
                      <div className="col">
                        <CardTitle
                          tag="h5"
                          className="text-uppercase text-muted mb-0"
                        >
                          Sürücü Sayısı
                        </CardTitle>
                        <span className="h2 font-weight-bold mb-0">{serviceLength}</span>
                      </div>
                      <Col className="col-auto">
                        <div className="icon icon-shape bg-warning text-white rounded-circle shadow">
                          <i className="fas fa-chart-pie" />
                        </div>
                      </Col>
                    </Row>
                  </CardBody>
                </Card>
              </Col>
              <Col lg="6" xl="3">
                <Card className="card-stats mb-4 mb-xl-0">
                  <CardBody>
                    <Row>
                      <div className="col">
                        <CardTitle
                          tag="h5"
                          className="text-uppercase text-muted mb-0"
                        >
                          Öğrenci Sayısı
                        </CardTitle>
                        <span className="h2 font-weight-bold mb-0">{studentCount}</span>
                      </div>
                      <Col className="col-auto">
                        <div className="icon icon-shape bg-yellow text-white rounded-circle shadow">
                          <i className="fas fa-users" />
                        </div>
                      </Col>
                      
                    </Row>
                  </CardBody>
                </Card>
              </Col>
            </Row>
          </div>
        </Container>
      </div>
    </>
  );
};

export default Header;
