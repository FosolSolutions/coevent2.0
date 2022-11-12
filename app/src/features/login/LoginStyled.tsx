import styled from 'styled-components';

export const Login = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: nowrap;
  justify-content: center;
  align-content: center;
  align-items: center;
  height: 100%;

  > div:first-child {
    display: flex;
    flex-direction: row;
    flex-flow: wrap;
    align-items: stretch;
    gap: 1em;
    border-radius: 0.5rem;
    background: white;
    padding: 1rem;
    height: max-content;
    max-width: 60ch;

    > * {
      flex: 1 0 100%;

      p {
        text-align: center;
      }
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

    input {
      min-width: 44ch;
    }
  }

  #user {
    display: flex;
    flex-direction: row;
    padding: 0.5em;
  }
`;

export default Login;
