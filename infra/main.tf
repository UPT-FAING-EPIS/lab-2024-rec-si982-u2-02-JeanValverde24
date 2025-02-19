provider "aws" {
  region = "us-east-1"
}

variable "instance_type" {
  default = "t3.micro" # Free Tier
}

variable "db_username" {
  default = "jeanvalverde"
}

variable "db_password" {
  default = "valverde24c++"
}

# Elastic Beanstalk Application con nombre fijo
resource "aws_elastic_beanstalk_application" "webapp" {
  name        = "eb-valverde"
  description = "Aplicación en Elastic Beanstalk"
}

# Elastic Beanstalk Environment sin Load Balancer (Single Instance)
resource "aws_elastic_beanstalk_environment" "webapp_env" {
  name                = "eb-env-valverde"
  application         = aws_elastic_beanstalk_application.webapp.name
  solution_stack_name = "64bit Amazon Linux 2023 v3.3.0 running .NET 8"

  # Modo "Single Instance" sin Load Balancer para reducir costos
  setting {
    namespace = "aws:elasticbeanstalk:environment"
    name      = "EnvironmentType"
    value     = "SingleInstance"
  }

  setting {
    namespace = "aws:autoscaling:launchconfiguration"
    name      = "InstanceType"
    value     = var.instance_type
  }

  setting {
    namespace = "aws:autoscaling:launchconfiguration"
    name      = "IamInstanceProfile"
    value     = "LabInstanceProfile"
  }

  setting {
    namespace = "aws:elasticbeanstalk:application:environment"
    name      = "RDS_HOSTNAME"
    value     = aws_db_instance.rds.address
  }

  setting {
    namespace = "aws:elasticbeanstalk:application:environment"
    name      = "RDS_DB_NAME"
    value     = aws_db_instance.rds.db_name
  }

  setting {
    namespace = "aws:elasticbeanstalk:application:environment"
    name      = "RDS_USERNAME"
    value     = var.db_username
  }

  setting {
    namespace = "aws:elasticbeanstalk:application:environment"
    name      = "RDS_PASSWORD"
    value     = var.db_password
  }
}

# Security Group con nombre fijo para Elastic Beanstalk y MySQL
resource "aws_security_group" "webapp_sg" {
  name        = "sg-valverde"
  description = "Security group para Elastic Beanstalk y MySQL"

  # Permitir tráfico HTTP
  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  # Permitir tráfico HTTPS
  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  # Permitir conexión a MySQL desde cualquier IP (⚠️ Ajustar si es necesario)
  ingress {
    from_port   = 3306
    to_port     = 3306
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"] # Permite acceso público a MySQL
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }
}

# RDS con MySQL en Free Tier, con nombre fijo
resource "aws_db_instance" "rds" {
  identifier             = "rds-valverde"
  engine                = "mysql"
  engine_version        = "8.0.35" # 💡 Versión estable compatible
  instance_class        = "db.t3.micro" # 💡 Free Tier
  allocated_storage     = 10  # Free Tier (mínimo permitido)
  username             = var.db_username
  password             = var.db_password
  publicly_accessible  = true  # Permite conexiones externas
  skip_final_snapshot  = true  # Evitar cargos extra
  vpc_security_group_ids = [aws_security_group.webapp_sg.id]
}
