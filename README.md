# Crypton

Este é um projeto de uma aplicação em console C# que possui algumas funcionalidades para encriptar ou embaralhar palavras. Para funcionar, o desenvolvedor deverá criar suas próprias chaves de criptografia, bem como o prefixo de embaralhamento de strings.

Edite as variáveis qwerty e qwertyuiop no arquivo [Criptografia.cs](CryptonPlugin/Criptografia.cs). A variável qwerty contém a chave criptográfica e a variável qwertyuiop contém o vetor de inicialização da criptografia (IV).

Para alguns algoritmos de encriptação, é necessário que as chaves possuam um tamanho fixo em bytes, por esta razão as chaves de texto puro que escolhemos como chave para encriptação não devem ser consideradas as chaves finais que serão utilizadas para encriptar os dados. O que se recomenda é que se aplique um algoritmo de HASH em nossas chaves de texto puro que gere um valor fixo em bytes e então este valor é que será usado como chave criptográfica.
