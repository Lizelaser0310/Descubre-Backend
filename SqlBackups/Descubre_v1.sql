--
-- PostgreSQL database dump
--

-- Dumped from database version 13.3 (Debian 13.3-1.pgdg100+1)
-- Dumped by pg_dump version 13.3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE IF EXISTS "Descubre";
--
-- Name: Descubre; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "Descubre" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';


ALTER DATABASE "Descubre" OWNER TO postgres;

\connect "Descubre"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: QuestionType; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public."QuestionType" AS ENUM (
    'Select',
    'RadioButton'
);


ALTER TYPE public."QuestionType" OWNER TO postgres;

--
-- Name: ResultState; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public."ResultState" AS ENUM (
    'Finished',
    'OnProgress',
    'Cancelled'
);


ALTER TYPE public."ResultState" OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Career; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Career" (
    "Id" integer NOT NULL,
    "Denomination" character varying(512) NOT NULL,
    "Information" smallint NOT NULL,
    "Duration" integer NOT NULL,
    "Created_at" timestamp with time zone NOT NULL,
    "Updated_at" timestamp with time zone,
    "Status" boolean NOT NULL
);


ALTER TABLE public."Career" OWNER TO postgres;

--
-- Name: Career_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Career" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Career_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Institution; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Institution" (
    "Id" integer NOT NULL,
    "Denomination" character varying(1024) NOT NULL,
    "Information" text NOT NULL,
    "Created_at" timestamp with time zone NOT NULL,
    "Updated_at" timestamp with time zone,
    "Status" boolean NOT NULL
);


ALTER TABLE public."Institution" OWNER TO postgres;

--
-- Name: InstitutionCareer; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."InstitutionCareer" (
    "Id" integer NOT NULL,
    "InstitutionId" integer NOT NULL,
    "CareerId" integer NOT NULL
);


ALTER TABLE public."InstitutionCareer" OWNER TO postgres;

--
-- Name: InstitutionCareer_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."InstitutionCareer" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."InstitutionCareer_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Institution_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Institution" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Institution_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Question; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Question" (
    "Id" integer NOT NULL,
    "TestId" integer NOT NULL,
    "Question" character varying(1024) NOT NULL,
    "Type" public."QuestionType" NOT NULL,
    "Alternatives" character varying(128)[] NOT NULL,
    "Weight" integer
);


ALTER TABLE public."Question" OWNER TO postgres;

--
-- Name: Question_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Question" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Question_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Recomendation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Recomendation" (
    "Id" integer NOT NULL,
    "ResultId" integer NOT NULL,
    "CareerId" integer NOT NULL,
    "Comments" text
);


ALTER TABLE public."Recomendation" OWNER TO postgres;

--
-- Name: Recomendation_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Recomendation" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Recomendation_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Response; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Response" (
    "Id" integer NOT NULL,
    "ResultId" integer NOT NULL,
    "QuestionId" integer NOT NULL,
    "AlternativeIdx" integer NOT NULL
);


ALTER TABLE public."Response" OWNER TO postgres;

--
-- Name: Response_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Response" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Response_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Result; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Result" (
    "Id" integer NOT NULL,
    "UserId" integer NOT NULL,
    "TestId" integer NOT NULL,
    "Status" public."ResultState" NOT NULL,
    "Score" integer NOT NULL,
    "StartDate" timestamp with time zone NOT NULL,
    "EndDate" timestamp with time zone NOT NULL
);


ALTER TABLE public."Result" OWNER TO postgres;

--
-- Name: Result_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Result" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Result_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Role" (
    "Id" integer NOT NULL,
    "Denomination" character varying(256) NOT NULL,
    "Created_at" timestamp with time zone NOT NULL,
    "Updated_at" timestamp with time zone,
    "Status" boolean NOT NULL
);


ALTER TABLE public."Role" OWNER TO postgres;

--
-- Name: Role_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Role" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Role_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: Test; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Test" (
    "Id" integer NOT NULL,
    "Denomination" character varying(512) NOT NULL,
    "AverageTime" integer NOT NULL,
    "Instructions" text,
    "Created_at" timestamp with time zone NOT NULL,
    "Updated_at" timestamp with time zone,
    "Status" boolean NOT NULL
);


ALTER TABLE public."Test" OWNER TO postgres;

--
-- Name: Test_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Test" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."Test_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: User; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."User" (
    "Id" integer NOT NULL,
    "RolId" integer NOT NULL,
    "Username" character varying(512) NOT NULL,
    "Password" character varying(512) NOT NULL,
    "DNI" character varying(8),
    "LastName" character varying(256) NOT NULL,
    "FirstName" character varying(256) NOT NULL,
    "Email" character varying(512) NOT NULL,
    "Birthday" timestamp with time zone NOT NULL,
    "Gender" character varying(1) NOT NULL,
    "Phone" character varying(32),
    "Status" boolean NOT NULL,
    "Foto" text,
    created_at timestamp with time zone NOT NULL,
    updated_at timestamp with time zone
);


ALTER TABLE public."User" OWNER TO postgres;

--
-- Name: User_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."User" ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."User_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Data for Name: Career; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: Institution; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: InstitutionCareer; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: Question; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: Recomendation; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: Response; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: Result; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: Role; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Role" ("Id", "Denomination", "Created_at", "Updated_at", "Status") OVERRIDING SYSTEM VALUE VALUES (1, 'Admin', '2021-06-27 14:59:29.135+00', NULL, true);
INSERT INTO public."Role" ("Id", "Denomination", "Created_at", "Updated_at", "Status") OVERRIDING SYSTEM VALUE VALUES (2, 'Expert', '2021-06-27 14:59:50.864+00', NULL, true);
INSERT INTO public."Role" ("Id", "Denomination", "Created_at", "Updated_at", "Status") OVERRIDING SYSTEM VALUE VALUES (3, 'Student', '2021-06-27 15:00:17.395+00', NULL, true);


--
-- Data for Name: Test; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: User; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."User" ("Id", "RolId", "Username", "Password", "DNI", "LastName", "FirstName", "Email", "Birthday", "Gender", "Phone", "Status", "Foto", created_at, updated_at) OVERRIDING SYSTEM VALUE VALUES (1, 1, 'Lizssdhdd', '+AXoghoSBoKpG3+TVi2PQNgobqoltadf|EXSZYsj/0+1aQpxxkaKNwG/u7tVNGmLI', '74379437', 'La Serna Felices', 'Lizeth Milagros', 'lizeth.lasernafelices@gmail.com', '1997-10-03 15:01:33.193+00', 'F', '933624871', true, NULL, '2021-06-27 18:05:44.354+00', NULL);
INSERT INTO public."User" ("Id", "RolId", "Username", "Password", "DNI", "LastName", "FirstName", "Email", "Birthday", "Gender", "Phone", "Status", "Foto", created_at, updated_at) OVERRIDING SYSTEM VALUE VALUES (3, 3, 'Gedzeppelin', 'l3f292HbX1/gAiNgXqlgYm7a5fhs5Ple|OXT1wmL/9rFo7k9864kWKKM4m6gLHK0S', '72499829', 'Palomino Naveros', 'Gedy Gedeon', 'gedy.palomino@gmail.com', '1996-07-24 14:48:00+00', 'M', '902729770', false, NULL, '0001-01-01 00:00:00+00', '2021-06-27 23:48:00.038734+00');


--
-- Name: Career_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Career_Id_seq"', 1, false);


--
-- Name: InstitutionCareer_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."InstitutionCareer_Id_seq"', 1, false);


--
-- Name: Institution_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Institution_Id_seq"', 1, false);


--
-- Name: Question_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Question_Id_seq"', 1, false);


--
-- Name: Recomendation_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Recomendation_Id_seq"', 1, false);


--
-- Name: Response_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Response_Id_seq"', 1, false);


--
-- Name: Result_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Result_Id_seq"', 1, false);


--
-- Name: Role_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Role_Id_seq"', 3, true);


--
-- Name: Test_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Test_Id_seq"', 1, false);


--
-- Name: User_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."User_Id_seq"', 3, true);


--
-- Name: Career Carrera_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Career"
    ADD CONSTRAINT "Carrera_pk" PRIMARY KEY ("Id");


--
-- Name: InstitutionCareer InstitutionCareer_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."InstitutionCareer"
    ADD CONSTRAINT "InstitutionCareer_pk" PRIMARY KEY ("Id");


--
-- Name: Institution Institution_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Institution"
    ADD CONSTRAINT "Institution_pk" PRIMARY KEY ("Id");


--
-- Name: Question Pregunta_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Question"
    ADD CONSTRAINT "Pregunta_pk" PRIMARY KEY ("Id");


--
-- Name: Test Prueba_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Test"
    ADD CONSTRAINT "Prueba_pk" PRIMARY KEY ("Id");


--
-- Name: Recomendation Recomendacion_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Recomendation"
    ADD CONSTRAINT "Recomendacion_pk" PRIMARY KEY ("Id");


--
-- Name: Response Respuesta_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Response"
    ADD CONSTRAINT "Respuesta_pk" PRIMARY KEY ("Id");


--
-- Name: Result Result_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Result"
    ADD CONSTRAINT "Result_pk" PRIMARY KEY ("Id");


--
-- Name: Role Rol_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Role"
    ADD CONSTRAINT "Rol_pk" PRIMARY KEY ("Id");


--
-- Name: User Usuario_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "Usuario_pk" PRIMARY KEY ("Id");


--
-- Name: InstitutionCareer Career_InstitutionCareer_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."InstitutionCareer"
    ADD CONSTRAINT "Career_InstitutionCareer_FK" FOREIGN KEY ("CareerId") REFERENCES public."Career"("Id") MATCH FULL;


--
-- Name: Recomendation Career_recomendation_Fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Recomendation"
    ADD CONSTRAINT "Career_recomendation_Fk" FOREIGN KEY ("CareerId") REFERENCES public."Career"("Id") MATCH FULL;


--
-- Name: InstitutionCareer Institution_InstitutionCareer_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."InstitutionCareer"
    ADD CONSTRAINT "Institution_InstitutionCareer_FK" FOREIGN KEY ("InstitutionId") REFERENCES public."Institution"("Id") MATCH FULL;


--
-- Name: Response Questions_Response_Fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Response"
    ADD CONSTRAINT "Questions_Response_Fk" FOREIGN KEY ("QuestionId") REFERENCES public."Question"("Id") MATCH FULL;


--
-- Name: Recomendation Result_Recomendation_Fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Recomendation"
    ADD CONSTRAINT "Result_Recomendation_Fk" FOREIGN KEY ("ResultId") REFERENCES public."Result"("Id") MATCH FULL;


--
-- Name: Response Result_Response_Fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Response"
    ADD CONSTRAINT "Result_Response_Fk" FOREIGN KEY ("ResultId") REFERENCES public."Result"("Id") MATCH FULL;


--
-- Name: User Role_User_FK; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."User"
    ADD CONSTRAINT "Role_User_FK" FOREIGN KEY ("RolId") REFERENCES public."Role"("Id") MATCH FULL;


--
-- Name: Question Test_Question_Fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Question"
    ADD CONSTRAINT "Test_Question_Fk" FOREIGN KEY ("TestId") REFERENCES public."Test"("Id") MATCH FULL;


--
-- Name: Result Test_User_Fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Result"
    ADD CONSTRAINT "Test_User_Fk" FOREIGN KEY ("TestId") REFERENCES public."Test"("Id") MATCH FULL;


--
-- Name: Result User_Result_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Result"
    ADD CONSTRAINT "User_Result_fk" FOREIGN KEY ("UserId") REFERENCES public."User"("Id") MATCH FULL;


--
-- PostgreSQL database dump complete
--

