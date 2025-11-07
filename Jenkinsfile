pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                bat '''
                    echo === Compilando solución ===
                    dotnet build PruebaTunit.sln --configuration Debug
                '''
            }
        }

        stage('Test') {
            steps {
                bat '''
                    echo === Ejecutando pruebas y generando XML JUnit ===
                    dotnet test testTunit/testTunit.csproj --no-build --logger "junit;LogFilePath=testTunit/TestResults/test_results.xml"
                    echo === Verificando archivo XML generado ===
                    dir testTunit\\TestResults
                '''
            }
            post {
                always {
                    echo '=== Publicando resultados JUnit ==='
                    junit 'testTunit/TestResults/*.xml'
                }
            }
        }
    }

    post {
        always {
            echo '=== Pipeline finalizado ==='
        }
    }
}
