pipeline {
    agent any

    stages {
        stage('Build') {
            steps {
                bat '''
                    echo === Etapa de compilación ===
                    dotnet build PruebaTunit.sln --configuration Debug
                '''
            }
        }

        stage('Test') {
            steps {
                bat '''
                    echo === Ejecutando pruebas unitarias ===
                    dotnet test testTunit/testTunit.csproj --no-build --configuration Debug --verbosity normal
                '''
            }
        }
    }

    post {
        success {
            echo '✅ Todas las pruebas pasaron correctamente.'
        }
        failure {
            echo '❌ Fallaron algunas pruebas.'
        }
        always {
            echo '=== Pipeline finalizado ==='
        }
    }
}

