import styled from 'styled-components';

export const Login = styled.div`
  height: 100%;
  display: flex;
  flex-direction: column;
  flex-wrap: nowrap;
  justify-content: center;
  align-content: center;
  align-items: center;
  gap: 1em;

  & > div:first-child {
    text-align: center;

    p {
      margin: 0.5em;
    }
  }

  form {
    border-radius: 0.25em;
    background-color: ${(props) => props.theme.css.formBackgroundColor};
  }

  #participant {
    display: flex;
    flex-direction: row;
    padding: 0.5em;
  }

  #user {
    display: flex;
    flex-direction: column;
    padding: 0.5em;
  }
`;

export default Login;
