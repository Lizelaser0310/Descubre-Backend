-- Database generated with pgModeler (PostgreSQL Database Modeler).
-- pgModeler  version: 0.9.3
-- PostgreSQL version: 13.0
-- Project Site: pgmodeler.io
-- Model Author: ---

-- Database creation must be performed outside a multi lined SQL file.
-- These commands were put in this file only as a convenience.
--
-- object: "Descubre" | type: DATABASE --
-- DROP DATABASE IF EXISTS "Descubre";
CREATE DATABASE "Descubre";
-- ddl-end --


-- object: public."Response" | type: TABLE --
-- DROP TABLE IF EXISTS public."Response" CASCADE;
CREATE TABLE public."Response" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"TestResultId" integer NOT NULL,
	"AlternativeId" integer NOT NULL,
	"Score" integer NOT NULL,
	CONSTRAINT "Response_PK" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Response" OWNER TO postgres;
-- ddl-end --

-- object: public."User" | type: TABLE --
-- DROP TABLE IF EXISTS public."User" CASCADE;
CREATE TABLE public."User" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"RolId" integer NOT NULL,
	"Username" varchar(512) NOT NULL,
	"Password" varchar(512) NOT NULL,
	"DNI" varchar(8),
	"LastName" varchar(256) NOT NULL,
	"FirstName" varchar(256) NOT NULL,
	"Email" varchar(512) NOT NULL,
	"Birthday" timestamptz NOT NULL,
	"Gender" varchar(1) NOT NULL,
	"Phone" varchar(32),
	"Created_at" timestamptz NOT NULL,
	"Updated_at" timestamptz,
	"Status" boolean NOT NULL,
	CONSTRAINT "Usuario_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."User" OWNER TO postgres;
-- ddl-end --

-- object: public."Career" | type: TABLE --
-- DROP TABLE IF EXISTS public."Career" CASCADE;
CREATE TABLE public."Career" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"Denomination" varchar(512) NOT NULL,
	"Information" text NOT NULL,
	"Duration" integer NOT NULL,
	"Created_at" timestamptz NOT NULL,
	"Updated_at" timestamptz,
	"Status" boolean NOT NULL,
	CONSTRAINT "Carrera_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Career" OWNER TO postgres;
-- ddl-end --

-- object: public."Role" | type: TABLE --
-- DROP TABLE IF EXISTS public."Role" CASCADE;
CREATE TABLE public."Role" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"Denomination" integer NOT NULL,
	"Created_at" timestamptz NOT NULL,
	"Updated_at" timestamptz,
	"Status" boolean NOT NULL,
	CONSTRAINT "Rol_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Role" OWNER TO postgres;
-- ddl-end --

-- object: public."Institution" | type: TABLE --
-- DROP TABLE IF EXISTS public."Institution" CASCADE;
CREATE TABLE public."Institution" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"Denomination" varchar(1024) NOT NULL,
	"Information" text NOT NULL,
	"Created_at" timestamptz NOT NULL,
	"Updated_at" timestamptz,
	"Status" boolean NOT NULL,
	CONSTRAINT "Institution_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Institution" OWNER TO postgres;
-- ddl-end --

-- object: public."Recomendation" | type: TABLE --
-- DROP TABLE IF EXISTS public."Recomendation" CASCADE;
CREATE TABLE public."Recomendation" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"ResultId" integer NOT NULL,
	"CareerId" integer NOT NULL,
	"Comments" text,
	CONSTRAINT "Recomendacion_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Recomendation" OWNER TO postgres;
-- ddl-end --

-- object: public."Test" | type: TABLE --
-- DROP TABLE IF EXISTS public."Test" CASCADE;
CREATE TABLE public."Test" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"Denomination" varchar(512) NOT NULL,
	"AverageTime" integer NOT NULL,
	"Instructions" text,
	"Created_at" timestamptz NOT NULL,
	"Updated_at" timestamptz,
	"Status" boolean NOT NULL,
	CONSTRAINT "Prueba_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Test" OWNER TO postgres;
-- ddl-end --

-- object: public."QuestionType" | type: TYPE --
-- DROP TYPE IF EXISTS public."QuestionType" CASCADE;
CREATE TYPE public."QuestionType" AS
 ENUM ('Select','RadioButton');
-- ddl-end --
ALTER TYPE public."QuestionType" OWNER TO postgres;
-- ddl-end --

-- object: public."Alternative" | type: TABLE --
-- DROP TABLE IF EXISTS public."Alternative" CASCADE;
CREATE TABLE public."Alternative" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"ModalityId" integer NOT NULL,
	"Denomination" varchar(1024) NOT NULL,
	"Weight" integer,
	"Created_at" timestamptz NOT NULL,
	"Updated_at" timestamptz,
	"Status" bool NOT NULL,
	CONSTRAINT "Pregunta_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Alternative" OWNER TO postgres;
-- ddl-end --

-- object: public."InstitutionCareer" | type: TABLE --
-- DROP TABLE IF EXISTS public."InstitutionCareer" CASCADE;
CREATE TABLE public."InstitutionCareer" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"InstitutionId" integer NOT NULL,
	"CareerId" integer NOT NULL,
	CONSTRAINT "InstitutionCareer_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."InstitutionCareer" OWNER TO postgres;
-- ddl-end --

-- object: public."ResultState" | type: TYPE --
-- DROP TYPE IF EXISTS public."ResultState" CASCADE;
CREATE TYPE public."ResultState" AS
 ENUM ('Finished','OnProgress','Cancelled');
-- ddl-end --
ALTER TYPE public."ResultState" OWNER TO postgres;
-- ddl-end --

-- object: public."Result" | type: TABLE --
-- DROP TABLE IF EXISTS public."Result" CASCADE;
CREATE TABLE public."Result" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"UserId" integer NOT NULL,
	"Status" public."ResultState" NOT NULL,
	"StartDate" timestamptz NOT NULL,
	"EndDate" timestamptz NOT NULL,
	CONSTRAINT "Result_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Result" OWNER TO postgres;
-- ddl-end --

-- object: public."Modality" | type: TABLE --
-- DROP TABLE IF EXISTS public."Modality" CASCADE;
CREATE TABLE public."Modality" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"TestId" integer NOT NULL,
	"Denomination" varchar(512) NOT NULL,
	"Description" text,
	"Image" text,
	"Created_at" timestamptz NOT NULL,
	"Updated_at" timestamptz,
	"Status" bool NOT NULL,
	CONSTRAINT "Modality_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."Modality" OWNER TO postgres;
-- ddl-end --

-- object: public."TestResult" | type: TABLE --
-- DROP TABLE IF EXISTS public."TestResult" CASCADE;
CREATE TABLE public."TestResult" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"ResultId" integer NOT NULL,
	"TestId" integer NOT NULL,
	"ModalityId" integer NOT NULL,
	"Total" integer NOT NULL,
	CONSTRAINT "TestResult_PK" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."TestResult" OWNER TO postgres;
-- ddl-end --

-- object: public."CareerModality" | type: TABLE --
-- DROP TABLE IF EXISTS public."CareerModality" CASCADE;
CREATE TABLE public."CareerModality" (
	"Id" integer NOT NULL GENERATED ALWAYS AS IDENTITY ,
	"CareerId" integer NOT NULL,
	"ModalityId" integer NOT NULL,
	"Weight" integer NOT NULL,
	CONSTRAINT "CareerModality_pk" PRIMARY KEY ("Id")

);
-- ddl-end --
ALTER TABLE public."CareerModality" OWNER TO postgres;
-- ddl-end --

-- object: "TestResult_Response_FK" | type: CONSTRAINT --
-- ALTER TABLE public."Response" DROP CONSTRAINT IF EXISTS "TestResult_Response_FK" CASCADE;
ALTER TABLE public."Response" ADD CONSTRAINT "TestResult_Response_FK" FOREIGN KEY ("TestResultId")
REFERENCES public."TestResult" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Alternative_Response_FK" | type: CONSTRAINT --
-- ALTER TABLE public."Response" DROP CONSTRAINT IF EXISTS "Alternative_Response_FK" CASCADE;
ALTER TABLE public."Response" ADD CONSTRAINT "Alternative_Response_FK" FOREIGN KEY ("AlternativeId")
REFERENCES public."Alternative" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Role_User_FK" | type: CONSTRAINT --
-- ALTER TABLE public."User" DROP CONSTRAINT IF EXISTS "Role_User_FK" CASCADE;
ALTER TABLE public."User" ADD CONSTRAINT "Role_User_FK" FOREIGN KEY ("RolId")
REFERENCES public."Role" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Result_Recomendation_Fk" | type: CONSTRAINT --
-- ALTER TABLE public."Recomendation" DROP CONSTRAINT IF EXISTS "Result_Recomendation_Fk" CASCADE;
ALTER TABLE public."Recomendation" ADD CONSTRAINT "Result_Recomendation_Fk" FOREIGN KEY ("ResultId")
REFERENCES public."Result" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Career_recomendation_Fk" | type: CONSTRAINT --
-- ALTER TABLE public."Recomendation" DROP CONSTRAINT IF EXISTS "Career_recomendation_Fk" CASCADE;
ALTER TABLE public."Recomendation" ADD CONSTRAINT "Career_recomendation_Fk" FOREIGN KEY ("CareerId")
REFERENCES public."Career" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "FK_Modality_Question" | type: CONSTRAINT --
-- ALTER TABLE public."Alternative" DROP CONSTRAINT IF EXISTS "FK_Modality_Question" CASCADE;
ALTER TABLE public."Alternative" ADD CONSTRAINT "FK_Modality_Question" FOREIGN KEY ("ModalityId")
REFERENCES public."Modality" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Institution_InstitutionCareer_FK" | type: CONSTRAINT --
-- ALTER TABLE public."InstitutionCareer" DROP CONSTRAINT IF EXISTS "Institution_InstitutionCareer_FK" CASCADE;
ALTER TABLE public."InstitutionCareer" ADD CONSTRAINT "Institution_InstitutionCareer_FK" FOREIGN KEY ("InstitutionId")
REFERENCES public."Institution" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Career_InstitutionCareer_FK" | type: CONSTRAINT --
-- ALTER TABLE public."InstitutionCareer" DROP CONSTRAINT IF EXISTS "Career_InstitutionCareer_FK" CASCADE;
ALTER TABLE public."InstitutionCareer" ADD CONSTRAINT "Career_InstitutionCareer_FK" FOREIGN KEY ("CareerId")
REFERENCES public."Career" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "User_Result_fk" | type: CONSTRAINT --
-- ALTER TABLE public."Result" DROP CONSTRAINT IF EXISTS "User_Result_fk" CASCADE;
ALTER TABLE public."Result" ADD CONSTRAINT "User_Result_fk" FOREIGN KEY ("UserId")
REFERENCES public."User" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "FK_Test_Modality" | type: CONSTRAINT --
-- ALTER TABLE public."Modality" DROP CONSTRAINT IF EXISTS "FK_Test_Modality" CASCADE;
ALTER TABLE public."Modality" ADD CONSTRAINT "FK_Test_Modality" FOREIGN KEY ("TestId")
REFERENCES public."Test" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Result_TestResult_FK" | type: CONSTRAINT --
-- ALTER TABLE public."TestResult" DROP CONSTRAINT IF EXISTS "Result_TestResult_FK" CASCADE;
ALTER TABLE public."TestResult" ADD CONSTRAINT "Result_TestResult_FK" FOREIGN KEY ("ResultId")
REFERENCES public."Result" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Test_TestResult_FK" | type: CONSTRAINT --
-- ALTER TABLE public."TestResult" DROP CONSTRAINT IF EXISTS "Test_TestResult_FK" CASCADE;
ALTER TABLE public."TestResult" ADD CONSTRAINT "Test_TestResult_FK" FOREIGN KEY ("TestId")
REFERENCES public."Test" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Modality_TestResult_FK" | type: CONSTRAINT --
-- ALTER TABLE public."TestResult" DROP CONSTRAINT IF EXISTS "Modality_TestResult_FK" CASCADE;
ALTER TABLE public."TestResult" ADD CONSTRAINT "Modality_TestResult_FK" FOREIGN KEY ("ModalityId")
REFERENCES public."Modality" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Career_CareerModality_FK" | type: CONSTRAINT --
-- ALTER TABLE public."CareerModality" DROP CONSTRAINT IF EXISTS "Career_CareerModality_FK" CASCADE;
ALTER TABLE public."CareerModality" ADD CONSTRAINT "Career_CareerModality_FK" FOREIGN KEY ("CareerId")
REFERENCES public."Career" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --

-- object: "Modality_CareerModality_FK" | type: CONSTRAINT --
-- ALTER TABLE public."CareerModality" DROP CONSTRAINT IF EXISTS "Modality_CareerModality_FK" CASCADE;
ALTER TABLE public."CareerModality" ADD CONSTRAINT "Modality_CareerModality_FK" FOREIGN KEY ("ModalityId")
REFERENCES public."Modality" ("Id") MATCH FULL
ON DELETE NO ACTION ON UPDATE NO ACTION;
-- ddl-end --


