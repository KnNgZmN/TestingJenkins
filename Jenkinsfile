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
                bat 'dotnet test --no-build --logger "trx;LogFileName=test_results.trx" --results-directory TestResults'
            }
            post {
                always {
                    junit 'TestResults/*.trx'
                }
            }
        }
        stage('Publish') {
            steps {
                bat 'dotnet publish -c Release -o out'
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

