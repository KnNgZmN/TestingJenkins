pipeline {
    agent any

    stages {

        stage('Restore') {
            steps {
                bat 'dotnet restore'
            }
        }

        stage('Build') {
            steps {
                bat 'dotnet build --no-restore'
            }
        }

        stage('Test') {
            steps {
                bat '''
                    rem === Ejecutar pruebas y generar archivo TRX ===
                    dotnet test --no-build --logger "trx;LogFileName=test_results.trx" --results-directory TestResults

                    rem === Instalar herramienta trx2junit si no existe ===
                    dotnet tool install -g trx2junit || echo "trx2junit ya instalado"

                    rem === Agregar las herramientas globales al PATH (solo necesario en Windows) ===
                    set PATH=%PATH%;%USERPROFILE%\\.dotnet\\tools

                    rem === Convertir los resultados TRX a XML (formato JUnit compatible con Jenkins) ===
                    trx2junit TestResults\\*.trx
                '''
            }
            post {
                always {
                    junit 'TestResults/*.xml'
                }
            }
        }

        stage('Publish') {
            steps {
                bat '''
                    dotnet publish -c Release -o out
                '''
                archiveArtifacts artifacts: 'out/**', fingerprint: true
            }
        }
    }

    post {
        success {
            echo '✅ Compilación y pruebas completadas con éxito.'
        }
        failure {
            echo '❌ Algo falló en la compilación o pruebas.'
        }
    }
}

